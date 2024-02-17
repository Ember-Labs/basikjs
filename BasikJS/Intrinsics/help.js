// [Intrinsics/help.js]
// Contains the global helper that will show useful information
// about BasikJS features

Basik.guide = (name) => {
    const indexes = {
        "workers:getShared": `
[bold fuchsia]Basik[/].[bold green]workers[/].[bold yellow]getShared[/]()

[yellow]<Sync Method>[/] [green]<Stable>[/] [red]<Unsafe>[/] <Last Updated: v0.0.1>

Provides access to the workers global shared object. Is stored inside the CLR application.
Only a single instance of the shared object will exist per application.
`,
        "workers:setShared": `
[bold fuchsia]Basik[/].[bold green]workers[/].[bold yellow]setShared[/](key: [bold yellow]string[/], value: [bold yellow]any[/])

[yellow]<Sync Method>[/] [green]<Stable>[/] [red]<Unsafe>[/] <Last Updated: v0.0.1>

Sets any value inside the workers global shared object. Not thread safe.
`,
    }

    if (!name) {
        return `
[bold green]Available[/] [bold yellow]guide[/] [bold green]options[/]

${Object.keys(indexes).join('\n')}`
    }

    return indexes[name];
}