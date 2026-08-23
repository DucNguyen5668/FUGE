import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "./globals.css";
import { Toaster } from "sonner";
import logoImage from "@/img/Logo.jpg";

const inter = Inter({ subsets: ["latin", "vietnamese"] });

export const metadata: Metadata = {
  title: "FuGrade Web — Hệ thống quản lý điểm",
  description: "FuGrade Web — Phiên bản web của FU Grading Editor",
  icons: {
    icon: logoImage.src,
    apple: logoImage.src,
  },
};

export default function RootLayout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="vi">
      <body
        suppressHydrationWarning
        className={`${inter.className} antialiased min-h-screen`}
      >
        {children}
        <Toaster richColors position="top-right" />
      </body>
    </html>
  );
}
