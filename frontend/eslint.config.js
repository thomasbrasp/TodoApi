import { fixupPluginRules } from "@eslint/compat";
import eslint from "@eslint/js";
import tanstackQuery from "@tanstack/eslint-plugin-query";
import pluginImport from "eslint-plugin-import";
import pluginPromise from "eslint-plugin-promise";
import pluginReact from "eslint-plugin-react";
import pluginReactHookForm from "eslint-plugin-react-hook-form";
import pluginReactHooks from "eslint-plugin-react-hooks";
import storybook from "eslint-plugin-storybook";
import { defineConfig } from "eslint/config";
import globals from "globals";
import tseslint from "typescript-eslint";

export default defineConfig([
    {
        ignores: ["node_modules/", "scripts", "dist/", "orval.config.cjs", "eslint.config.js", "src/lib/typings/**/*.ts", "src/gql/**/*.ts"], // TODO: Integrate typings with eslint + prettier
    },
    eslint.configs.recommended,
    tseslint.configs.strictTypeChecked,
    tseslint.configs.stylisticTypeChecked,
    pluginReact.configs.flat.recommended,
    pluginReactHooks.configs["recommended-latest"],
    ...tanstackQuery.configs["flat/recommended"],
    pluginImport.flatConfigs.recommended,
    pluginImport.flatConfigs.typescript,
    pluginPromise.configs["flat/recommended"],
    ...storybook.configs["flat/recommended"],
    {
        files: ["**/*.{js,ts,mjs,mts,cjs,cts,jsx,tsx}"],
        languageOptions: {
            globals: globals.browser,
            ecmaVersion: "latest",
            sourceType: "module",
            parserOptions: {
                ecmaFeatures: {
                    jsx: true,
                },
                projectService: true,
                tsconfigRootDir: import.meta.dirname,
            },
        },
        settings: {
            react: { version: "detect" },
            "import/resolver": {
                typescript: {
                    project: "./tsconfig.json",
                },
            },
        },
        plugins: {
            pluginReact,
            "react-hook-form": fixupPluginRules(pluginReactHookForm),
        },
        rules: {
            "@typescript-eslint/no-misused-promises": ["off"], // TODO: react elements (button, ...) don't support promises
            "@typescript-eslint/no-unsafe-member-access": "off", // TODO: Fix
            "@typescript-eslint/no-unsafe-argument": "off", // TODO: Fix
            "@typescript-eslint/no-unsafe-call": "off", // TODO: Fix
            "@typescript-eslint/no-unsafe-return": "off", // TODO: Fix
            "@typescript-eslint/no-unsafe-assignment": "off", // TODO: Fix
            "@typescript-eslint/no-explicit-any": "off", // TODO: Fix
            "@typescript-eslint/no-confusing-void-expression": "off", // TODO: Fix
            "@typescript-eslint/prefer-nullish-coalescing": "off", // TODO: Fix
            "@typescript-eslint/no-unnecessary-condition": "off", // TODO: Fix later
            "@typescript-eslint/no-misused-spread": "off", // TODO: Fix later
            "@typescript-eslint/no-non-null-assertion": "off", // TODO: Fix later,
            "@typescript-eslint/no-unnecessary-type-conversion": "off", // TODO: Fix later
            "arrow-body-style": "off", // TODO: Fix later
            "@typescript-eslint/await-thenable": "off", // TODO: Fix later
            "@typescript-eslint/prefer-optional-chain": "off", // TODO: Fix later
            "@typescript-eslint/restrict-template-expressions": ["error", { allowBoolean: true, allowNumber: true }],
            "@typescript-eslint/no-floating-promises": "error",
            "@typescript-eslint/no-unused-vars": ["error"],
            "no-await-in-loop": "error",
            "no-console": "error",
            "react/jsx-no-constructed-context-values": "error",
            "react/no-danger": "error",
            "react/no-unstable-nested-components": ["error", { allowAsProps: true }],
            "react/react-in-jsx-scope": "off",
            "require-await": "error",
            "react-hooks/exhaustive-deps": "error",
            "no-restricted-imports": [
                "error",
                {
                    paths: [
                        {
                            name: "@mui/lab",
                            importNames: ["LoadingButton", "LoadingButtonProps"],
                            message: "Please import our custom BCT components",
                        },
                        {
                            name: "@mui/material",
                            importNames: ["Button", "ButtonProps"],
                            message: "Please import our custom BCT components",
                        },
                    ],
                },
            ],
        },
    },
]);
