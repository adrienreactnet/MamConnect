# MamConnect API

## Authentication

### `POST /auth/register`

Registers a new user. The body should contain only an email and password:

```json
{
  "email": "user@example.com",
  "password": "string"
}
```

New accounts are created with the `Parent` role by default.