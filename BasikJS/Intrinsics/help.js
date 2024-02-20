// [Intrinsics/help.js]
// Contains the global helper that will show useful information
// about BasikJS features

Basik.guide = (name) => {
    const indexes = {
        "console:log": _basikJsInternals_help_console_log,
        "console:read": _basikJsInternals_help_console_read,
        "workers:getShared": _basikJsInternals_help_getSharedText,
        "workers:setShared": _basikJsInternals_help_setSharedText,
        "pipelines:createCommand": _basikJsInternals_help_pipelines_createCommand
    }

    if (!name) {
        console.log(`[bold green]Available[/] [bold yellow]guide[/] [bold green]options[/]

${Object.keys(indexes).join('\n')}`);
        return;
    }

    console.log(indexes[name]);
}