// DOM Elements
const statusIndicator = document.getElementById('statusIndicator');
const statusText = document.getElementById('statusText');
const statusMessage = document.getElementById('statusMessage');
const serverStatus = document.getElementById('serverStatus');
const serverTimestamp = document.getElementById('serverTimestamp');
const checkHealthBtn = document.getElementById('checkHealthBtn');
const consoleOutput = document.getElementById('consoleOutput');
const clearLogsBtn = document.getElementById('clearLogsBtn');

// UI State Management
class UIManager {
    updateStatus(isHealthy, message = '') {
        if (isHealthy) {
            statusIndicator.className = 'status-indicator healthy';
            statusText.textContent = 'Healthy';
            statusMessage.textContent = message || 'Backend is running smoothly';
        } else {
            statusIndicator.className = 'status-indicator unhealthy';
            statusText.textContent = 'Unhealthy';
            statusMessage.textContent = message || 'Cannot connect to backend';
        }
    }

    updateServerInfo(data) {
        if (data) {
            serverStatus.textContent = data.status || 'Unknown';
            serverTimestamp.textContent = data.timestamp 
                ? new Date(data.timestamp).toLocaleString() 
                : '-';
        } else {
            serverStatus.textContent = '-';
            serverTimestamp.textContent = '-';
        }
    }

    addConsoleLog(logEntry) {
        const logElement = document.createElement('div');
        logElement.className = `console-message ${logEntry.type}`;
        
        let icon = '📝';
        if (logEntry.type === 'success') icon = '✅';
        if (logEntry.type === 'error') icon = '❌';
        if (logEntry.type === 'warning') icon = '⚠️';
        
        const dataString = logEntry.data ? JSON.stringify(logEntry.data, null, 2) : '';
        
        logElement.innerHTML = `
            <span class="log-time">[${logEntry.timestamp}]</span>
            <span class="log-icon">${icon}</span>
            <span class="log-text">${logEntry.message}</span>
            ${dataString ? `<pre class="log-data">${dataString}</pre>` : ''}
        `;
        
        consoleOutput.appendChild(logElement);
        consoleOutput.scrollTop = consoleOutput.scrollHeight;
    }

    clearConsole() {
        consoleOutput.innerHTML = '<div class="console-message info">Console cleared. Ready for new logs...</div>';
    }

    setButtonLoading(isLoading) {
        checkHealthBtn.disabled = isLoading;
        checkHealthBtn.innerHTML = isLoading 
            ? '<span class="spinner"></span> Checking...' 
            : `<svg width="16" height="16" viewBox="0 0 16 16" fill="none">
                <path d="M13.65 2.35A8 8 0 1 0 2.35 13.65 8 8 0 1 0 13.65 2.35z" stroke="currentColor" stroke-width="1.5"/>
                <path d="M8 4v4l3 3" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"/>
               </svg> Check Health`;
    }
}

const uiManager = new UIManager();

// Health Check Function
async function performHealthCheck() {
    uiManager.setButtonLoading(true);
    uiManager.updateStatus(false, 'Checking...');
    
    const result = await api.checkHealth();
    
    if (result.success) {
        uiManager.updateStatus(true, `Backend responded in ${result.duration}ms`);
        uiManager.updateServerInfo(result.data);
    } else {
        uiManager.updateStatus(false, result.error);
        uiManager.updateServerInfo(null);
    }
    
    uiManager.setButtonLoading(false);
}

// Event Listeners
checkHealthBtn.addEventListener('click', performHealthCheck);

clearLogsBtn.addEventListener('click', () => {
    logger.clear();
});

window.addEventListener('apiLog', (event) => {
    uiManager.addConsoleLog(event.detail);
});

window.addEventListener('apiLogClear', () => {
    uiManager.clearConsole();
});

// Auto-check on page load
window.addEventListener('DOMContentLoaded', () => {
    console.log('%c🚀 Meetup Frontend Initialized', 'color: #00ff88; font-size: 16px; font-weight: bold;');
    console.log('%cAll API calls will be logged here and in the UI', 'color: #888; font-size: 12px;');
    
    // Perform initial health check after a short delay
    setTimeout(() => {
        performHealthCheck();
    }, 500);
});
