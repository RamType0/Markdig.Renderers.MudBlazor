import * as esbuild from "esbuild"
import pkg from './package.json' assert { type: 'json' }

await esbuild.build({
    entryPoints: ["./wwwroot/**/*.ts", "**/*.razor.ts"],
    outbase: "./",
    outdir: "./",
    bundle: true,
    minify: true,
    sourcemap: true,
    format:"esm",
    platform: "browser",
    target: ['chrome132', 'firefox134', 'safari18', 'edge132'],
})