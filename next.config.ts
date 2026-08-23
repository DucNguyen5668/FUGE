import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  serverExternalPackages: ["better-sqlite3", "exceljs"],
  turbopack: {
    root: process.cwd(),
  },
};

export default nextConfig;
