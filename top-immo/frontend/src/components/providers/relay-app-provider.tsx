"use client";

import { ReactNode, useMemo } from "react";
import { RelayEnvironmentProvider } from "react-relay";
import { createRelayEnvironment } from "@/lib/relay/environment";

type RelayAppProviderProps = {
  children: ReactNode;
};

export function RelayAppProvider({ children }: RelayAppProviderProps) {
  const environment = useMemo(() => createRelayEnvironment(), []);

  return (
    <RelayEnvironmentProvider environment={environment}>
      {children}
    </RelayEnvironmentProvider>
  );
}
