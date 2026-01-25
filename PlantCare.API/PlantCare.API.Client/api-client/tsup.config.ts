import { defineConfig } from 'tsup'

export default defineConfig({
    entry: ['./index.ts'],
    format: ['cjs'],
    dts: true,
    clean: true,
    shims: true,
    skipNodeModulesBundle: true,
})
