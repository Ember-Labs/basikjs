const console = (() => {
    class Console {
        getReadings = () => null;
        getRecords = () => null;
        _interopObject;

        constructor(enableDebugMode) {
            this._interopObject = _basikJsInternals_createConsole(enableDebugMode);
            this.getReadings = () => {
                return Array.from(this._interopObject.readings);
            }
            this.getRecords = () => {
                return Array.from(this._interopObject.records);
            }
        }

        log(...args) {
            this._interopObject.log(...args);
        }

        read() {
            return this._interopObject.read();
        }

        clear() {
            this._interopObject.clear();
        }
    }

    return new Console(true);
})();