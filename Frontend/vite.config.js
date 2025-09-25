import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import fs from 'fs';
import path from 'path';

const certPath = process.env.VITE_CERT_PATH || './cert/localhost+2.pem'
const keyPath = process.env.VITE_KEY_PATH || './cert/localhost+2-key.pem'

export default defineConfig({
  plugins: [react()],
  server: {
    https: {
      key: fs.readFileSync(keyPath),
      cert: fs.readFileSync(certPath)
    },
    host: true,
    port: 5173,
  },
});


vite.config.js