import { Environment, Network, RecordSource, Store } from "relay-runtime";

const endpoint =
  process.env.NEXT_PUBLIC_GRAPHQL_ENDPOINT ?? "http://localhost:5224/graphql";

async function fetchGraphQL(text: string | null | undefined, variables: object) {
  const response = await fetch(endpoint, {
    method: "POST",
    headers: {
      "content-type": "application/json",
      accept: "application/json"
    },
    body: JSON.stringify({
      query: text,
      variables
    })
  });

  return response.json();
}

export function createRelayEnvironment() {
  return new Environment({
    network: Network.create((operation, variables) =>
      fetchGraphQL(operation.text, variables)
    ),
    store: new Store(new RecordSource())
  });
}
