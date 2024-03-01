export const console = (() => {
    class Console {
        getReadings: () => string[];
        getRecords: () => string[];
        _interopObject: any;

        constructor(enableDebugMode: boolean) {
            this._interopObject = __internalInterop['create-console'](enableDebugMode);
            this.getReadings = () => {
                return Array.from(this._interopObject.readings);
            }
            this.getRecords = () => {
                return Array.from(this._interopObject.records);
            }
        }

        log(...args: any[]) {
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
})()