import { defineConfig, loadEnv } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/

export default defineConfig(({ mode }) => {

    const env = loadEnv(mode, process.cwd(), "");

    const gatewayUrl = env.GATEWAY_URL || "https://localhost:5000";

    return {
        plugins: [react()],
        server: {
            port: 7000,
            strictPort: true,
            proxy: {
                "/bff": {
                    target: gatewayUrl,
                    changeOrigin: false,
                    secure: false,
                    headers: {
                        "X-Forwarded-Host": "localhost:7000",
                        "X-Forwarded-Proto": "http"
                    }
                },

                "/v1": {
                    target: gatewayUrl,
                    changeOrigin: false,
                    secure: false,
                    ws: true,
                    headers: {
                        "X-Forwarded-Host": "localhost:7000",
                        "X-Forwarded-Proto": "http"
                    }
                },

                "/signin-oidc": {
                    target: gatewayUrl,
                    changeOrigin: false,
                    secure: false,
                    headers: {
                        "X-Forwarded-Host": "localhost:7000",
                        "X-Forwarded-Proto": "http"
                    }
                },

                "/signout-callback-oidc": {
                    target: gatewayUrl,
                    changeOrigin: false,
                    secure: false,
                    headers: {
                        "X-Forwarded-Host": "localhost:7000",
                        "X-Forwarded-Proto": "http"
                    }
                }
            }
        }
    };
});