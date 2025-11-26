# Meetup Frontend

A minimal dark-themed frontend application for testing the Meetup backend API health.

## Features

- **Health Check Dashboard**: Visual status indicator for backend connectivity
- **Real-time Logging**: All API calls are logged to both the browser console and UI console
- **Dark Mode Design**: Clean, minimal dark theme for better readability
- **Responsive Layout**: Works on desktop and mobile devices

## Getting Started

### Prerequisites

- A modern web browser (Chrome, Firefox, Safari, Edge)
- Meetup Backend API running on `http://localhost:5000`

### Running the Application

Since this is a static HTML/CSS/JS application, you can run it in several ways:

#### Option 1: Using Live Server (Recommended)

If you have the Live Server extension in VS Code:
1. Right-click on `index.html`
2. Select "Open with Live Server"

#### Option 2: Using Python HTTP Server

```bash
cd frontend
python -m http.server 8080
```

Then open `http://localhost:8080` in your browser.

#### Option 3: Using Node.js HTTP Server

```bash
cd frontend
npx http-server -p 8080
```

Then open `http://localhost:8080` in your browser.

#### Option 4: Direct File Opening

Simply open `index.html` directly in your browser. Note: Some features may not work due to CORS restrictions.

## Project Structure

```
frontend/
├── index.html          # Main HTML page
├── css/
│   └── styles.css     # Dark mode styles
├── js/
│   ├── api.js         # API service and logging
│   └── app.js         # UI logic and event handlers
└── README.md
```

## Features

### Health Check
- Visual status indicator (green = healthy, red = unhealthy)
- Displays server timestamp from the backend
- Shows response time for API calls

### Console Logging
- All API requests and responses are logged
- Color-coded log messages (info, success, error, warning)
- Viewable in both UI console and browser console (F12)
- Clear logs functionality

### Dark Mode Design
- Minimal, clean interface
- Easy on the eyes for extended use
- Responsive design for various screen sizes

## API Configuration

The frontend is configured to connect to:
- **Backend URL**: `http://localhost:5000`
- **Health Endpoint**: `/api/health`

To change the API URL, edit `js/api.js`:
```javascript
const API_BASE_URL = 'http://localhost:5000';
```

## Browser Console

Press `F12` to open the browser's developer tools and view detailed logs of all API interactions.
