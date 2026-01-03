# Finite State Machine

A flexible, type-safe and efficient implementation of [finite state machine](https://en.wikipedia.org/wiki/Finite-state_machine) written using C# 14 (.NET 10). It is designed to be generic, extensible, and easy to construct with a fluent builder pattern, and generally aimed to use in games.

## Features

- **Generic State Keys:** Use any non-nullable type as the key for your states.
- **Lifecycle Methods:** States support `Enter`, `Exit`, and `Update` methods to override.
- **Efficient State Lookup:** Uses immutable dictionaries for fast state transitions.
- **Fluent Builder API:** Easily construct FSMs with a clear and concise syntax.

## License

This project is licensed under the terms of the MIT license. See the [LICENSE](LICENSE) file for details.
