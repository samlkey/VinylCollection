from flask import Flask, jsonify
import requests
import urllib.parse
import re
import os

app = Flask(__name__)
client_id = '3525067d698c4dc8afcf412b1ac092cd'
client_secret = 'ce702c554c3e459a830ccb3a414adf3b'
albums = {
    "OK COMPUTER" : "RADIOHEAD",
    "MM..FOOD" : "MF DOOM",
}

@app.route('/api/album_list')
def album_list():
    return jsonify(albums)

@app.route('/api/album_info/<title>', methods=['GET'])
def album_info(title):
    token = auth()
    if not token:
        return jsonify({"error": "Could not authenticate with Spotify"}), 500
    headers = {
        "Authorization": f"Bearer {token}"
    }
    params = {
        "q": title,
        "type": "album",
        "limit": 1
    }
    response = requests.get("https://api.spotify.com/v1/search", headers=headers, params=params)
    response.raise_for_status()
    data = response.json()
    album_items = data.get("albums", {}).get("items", [])
    if not album_items:
        return jsonify({"error": "Album not found"}), 404
    album = album_items[0]
    # Get the first image URL
    image_url = album.get("images", [{}])[0].get("url")
    release_date = album.get("release_date")
    total_tracks = album.get("total_tracks")
    spotify_url = album.get("external_urls", {}).get("spotify")
    return jsonify({
        "cover_image": image_url,
        "release_date": release_date,
        "total_tracks": total_tracks,
        "spotify_url": spotify_url
    })

def auth():
    url = "https://accounts.spotify.com/api/token"
    headers = {
        "Content-Type": "application/x-www-form-urlencoded"
    }
    data = {
        "grant_type": "client_credentials",
        "client_id": client_id,
        "client_secret": client_secret
    }
    response = requests.post(url, headers=headers, data=data)
    if response.status_code == 200:
        return response.json()["access_token"]
    else:
        return jsonify({"error": "Spotify auth failed", "details": response.text}), 500

@app.route('/')
def home():
    return 'Hello, Flask!'

if __name__ == '__main__':
    app.run(debug=True) 
#find api for album data