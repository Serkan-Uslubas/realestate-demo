import { PropertyList } from "@/components/property-list";
import { Suspense } from "react";

export default function HomePage() {
  return (
    <main className="page">
      <section className="hero">
        <p className="tag">top.immo</p>
        <h1>Immobilien uebersichtlich verwalten</h1>
        <p>
          Diese Startbasis laedt die Objektdaten direkt per GraphQL aus deinem
          C# Backend.
        </p>
      </section>

      <Suspense fallback={<p>Lade Immobilien...</p>}>
        <PropertyList />
      </Suspense>
    </main>
  );
}
