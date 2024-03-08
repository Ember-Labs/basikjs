# 🏹 BasikJS

BasikJS is a ***TypeScript*** runtime written in modern .NET, focused on DevOps, server management and automation. It aims to provide a full set of features to ease pipeline tasks profiting from high parallelism and optimal usage of the device resources while keeping the simplicity and usability of JavaScript/TypeScript. Basik counts with its own spec and set of carefully selected functionalities to provide an optimal experience.

> *In development*

## Using shell commands

```ts
const { command } = Basik.pipelines;

const handler = await command.open({ shellFallback: ['bash'] });
await command.run('git', ['pull']);
```

## Architecture Sketches

### External tool usage

- [Vite] -> [Pre-compiling?]
- [Jint] -> [JavaScript interpreter]
- [CliWrap] -> [Cli operations]
- [Spectre] -> [Cli beautify/utility]

### Worker manual garbage collection

- [Front worker] -> [Default JS Garbage collection]
- [IO worker] -> [Manual Garbage collection through free api?]
- [Interop worker] -> [Manual Garbage collection through free api?]

### General todo

- [ ] Command line invoker
- [ ] Fixed command line to be cross platform
- [ ] IO Module with FileSystem, HTTP(S), FTP, SSH
