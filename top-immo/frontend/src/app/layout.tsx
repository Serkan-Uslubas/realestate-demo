import "./globals.css";
import type { Metadata } from "next";
import { RelayAppProvider } from "@/components/providers/relay-app-provider";

export const metadata: Metadata = {
  title: "top.immo",
  description: "Immobilienplattform mit Next.js, C# und GraphQL"
};

export default function RootLayout({
  children
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="de">
      <body>
        <RelayAppProvider>{children}</RelayAppProvider>
      </body>
    </html>
  );
}
