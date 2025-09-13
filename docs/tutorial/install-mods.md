---
sidebar_position: 2
---

# Installing Mods

This tutorial will guide you on how to install mods into the game.

This article uses the **SampleHook** mod as an example to demonstrate how to install **mods**.

:::info
The **Mods Directory** mentioned in this article is located at `<DeadCellsGameRoot>/coremod/mods`.
:::

## Getting Mods

You can get **mods** from any source you prefer.

:::tip
For any **valid** mod, a `modinfo.json` file should exist in its root directory.
For example:

```txt
SampleHook
├─ modinfo.json
├─ SampleHook.dll
└─ SampleHook.pdb
```

:::

## Copying the Mod Files

Copy the mod folder into the Mods Directory.

:::tip

After completing the above steps, the directory structure should look something like this:

```
<DeadCellsGameRoot>
├─ coremod
│  ├─ mods
│  │  ├─ SampleHook
|  |  |  ├─ modinfo.json
|  |  |  └─ …
|  |  └─ …
|  └─ …
└─ …
```

:::

## Common Issues

### Q1: Mod fails to load

Confirm that the modinfo.json file format is correct (you can use a JSON validation tool).

Check the game log for error messages.

### Q2: Mod has no effect after loading

Check the game log for any warnings or errors.

### Q3: Multiple mods are conflicting

Check the mod dependencies.

Try enabling mods one by one to troubleshoot.

Check the game log for any warnings or errors.
