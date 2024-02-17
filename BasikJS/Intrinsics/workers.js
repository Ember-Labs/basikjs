// [Intrinsics/workers.js]
// Contains the global definitions for BasikJs, placeholding
// all the callable definitions to be futurely set up by the
// internal interop setup

Basik.workers.getShared = () => {
    return _basikJsInternals_workers_getSharedRaw();
};

Basik.workers.setShared = (key, value) => {
    _basikJsInternals_workers_setSharedRaw(key, value);
}