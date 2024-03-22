class Console {
    private interopObject: any;

    constructor(enableDebugMode: boolean) {
        this.interopObject = __internalInterop['create-console'](enableDebugMode);
    }

    public getReadings() {
        return Array.from<string>(this.interopObject.readings)
    }

    public getRecords() {
        return Array.from<string>(this.interopObject.records);
    }

    log(...args: any[]): void {
        this.interopObject.log(...args);
    }

    read(): string | null {
        return this.interopObject.read();
    }

    clear(): void {
        this.interopObject.clear();
    }
}

export const console = new Console(true);