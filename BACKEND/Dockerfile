# Use the official Python image
FROM python:3.12-slim

# Set work directory
WORKDIR /app

# Install dependencies
COPY requirements.txt ./
RUN pip install --no-cache-dir -r requirements.txt

# Copy the rest of the code
COPY . .

# Expose the port Flask runs on
EXPOSE 5000

# Set environment variables for Flask hot reload
ENV FLASK_APP=app.py
ENV FLASK_ENV=development

# Run the application with hot reload
CMD ["flask", "run", "--host=0.0.0.0"] 