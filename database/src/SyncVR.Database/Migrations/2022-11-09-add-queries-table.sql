CREATE TABLE fibonacci_series (
    position INTEGER NOT NULL PRIMARY KEY UNIQUE,
    value BIGINT NOT NULL
);

CREATE TABLE queries (
    id UUID NOT NULL PRIMARY KEY DEFAULT GEN_RANDOM_UUID(),
    created_at TIMESTAMPTZ NOT NULL,
    client_id VARCHAR(48) NOT NULL,
    fibonacci_position INTEGER NOT NULL
        REFERENCES fibonacci_series (position)
        ON DELETE CASCADE
);
