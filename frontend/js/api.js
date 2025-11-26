// API Configuration
const API_BASE_URL = 'http://localhost:5000';
const API_ENDPOINTS = {
    health: '/api/health'
};

// Logger utility
class ApiLogger {
    constructor() {
        this.logs = [];
    }

    log(type, message, data = null) {
        const timestamp = new Date().toLocaleTimeString();
        const logEntry = {
            timestamp,
            type,
            message,
            data
        };
        
        this.logs.push(logEntry);
        
        // Log to browser console
        const consoleMethod = type === 'error' ? 'error' : type === 'warning' ? 'warn' : 'log';
        console[consoleMethod](`[${timestamp}] ${message}`, data || '');
        
        // Dispatch custom event for UI updates
        window.dispatchEvent(new CustomEvent('apiLog', { detail: logEntry }));
    }

    clear() {
        this.logs = [];
        console.clear();
        window.dispatchEvent(new CustomEvent('apiLogClear'));
    }
}

const logger = new ApiLogger();

// API Service
class ApiService {
    constructor(baseUrl) {
        this.baseUrl = baseUrl;
    }

    async request(endpoint, options = {}) {
        const url = `${this.baseUrl}${endpoint}`;
        const method = options.method || 'GET';
        
        logger.log('info', `🚀 API Request: ${method} ${url}`);
        
        const startTime = performance.now();
        
        try {
            const response = await fetch(url, {
                method,
                headers: {
                    'Content-Type': 'application/json',
                    ...options.headers
                },
                ...options
            });

            const endTime = performance.now();
            const duration = Math.round(endTime - startTime);

            logger.log('info', `⏱️ Response received in ${duration}ms`, {
                status: response.status,
                statusText: response.statusText
            });

            if (!response.ok) {
                throw new Error(`HTTP ${response.status}: ${response.statusText}`);
            }

            const data = await response.json();
            
            logger.log('success', `✅ Request successful`, data);
            
            return {
                success: true,
                data,
                status: response.status,
                duration
            };

        } catch (error) {
            const endTime = performance.now();
            const duration = Math.round(endTime - startTime);
            
            logger.log('error', `❌ Request failed: ${error.message}`, {
                error: error.message,
                duration
            });
            
            return {
                success: false,
                error: error.message,
                duration
            };
        }
    }

    async checkHealth() {
        logger.log('info', '🏥 Checking backend health...');
        return await this.request(API_ENDPOINTS.health);
    }
}

// Export API service instance
const api = new ApiService(API_BASE_URL);
