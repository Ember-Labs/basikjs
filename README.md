# BasikJS

BasikJS is a ***TypeScript*** runtime written in modern .NET, focused on DevOps, server management and automation. It aims to provide a full set of features to ease pipeline tasks profiting from massive parallelism and optimal usage of the device resources while keeping the simplicity and usability of JavaScript/TypeScript. Basik counts with its own spec and set of carefully selected functionalities to provide an optimal experience.

> *In development*

## Architecture Sketches

### Worker manual garbage collection

- [Front worker] -> [Default JS Garbage collection]
- [IO worker] -> [Manual Garbage collection through free api?]
- [Interop worker] -> [Manual Garbage collection through free api?]
