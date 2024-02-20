// [Intrinsics/global.js]
// Contains the global definitions for BasikJs and globals, placeholding
// all the callable definitions to be futurely set up by the
// internal interop setup

const console = {
    log: "not-initialized",
    clear: "not-initialized",
    read: "not-initialized" 
}

const Basik = {
    currentVersion: '0.0.1',
    guide: "not-initialized",
    workers: {
        getShared: "not-initialized",
        setShared: "not-initialized"
    },
    pipelines: {
        createCommand: "not-initialized"
    }
};