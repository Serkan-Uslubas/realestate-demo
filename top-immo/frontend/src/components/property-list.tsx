"use client";

import { graphql, useLazyLoadQuery } from "react-relay";

const propertyListQuery = graphql`
  query propertyListQuery($take: Int!) {
    properties(take: $take) {
      id
      slug
      title
      city
      price
      offerType
      bedrooms
      bathrooms
      areaSqm
    }
  }
`;

export function PropertyList() {
  const data = useLazyLoadQuery<any>(
    propertyListQuery,
    { take: 24 },
    { fetchPolicy: "store-and-network" }
  );

  if (!data?.properties?.length) {
    return <p>Keine Immobilien gefunden.</p>;
  }

  return (
    <section className="property-grid">
      {data.properties.map((item: any) => (
        <article key={item.id} className="property-card">
          <h3>{item.title}</h3>
          <p className="property-meta">
            {item.city} | {item.offerType}
          </p>
          <p className="property-meta">
            {item.bedrooms} Zi. | {item.bathrooms} Bad | {item.areaSqm} m2
          </p>
          <p className="price">{formatEuro(item.price)}</p>
        </article>
      ))}
    </section>
  );
}

function formatEuro(value: string | number) {
  const numericValue = typeof value === "string" ? Number(value) : value;

  return new Intl.NumberFormat("de-DE", {
    style: "currency",
    currency: "EUR",
    maximumFractionDigits: 0
  }).format(numericValue ?? 0);
}
