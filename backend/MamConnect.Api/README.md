# MamConnect API

## Authentication

### `POST /auth/register`

Creates a new user. This endpoint requires authorization.

```json
{
  "email": "user@example.com",
  "firstName": "First",
  "lastName": "Last",
  "phoneNumber": "+1234567890",
  "role": "Parent"
}
```

Returns `204 No Content` on success.