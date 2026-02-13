import {defineConfig} from 'orval';

export default defineConfig({
    todo: {
        output: {
            mode: 'single',
            target: './src/api/endpoints/todo.ts',
            client: "react-query",
            httpClient: "axios",
            override: {
                mutator: {
                    path: "./src/config/http.ts",
                    name: "todoInstance",
                }
            },
        },
        input: "http://localhost:5062/swagger/v1/swagger.json/",
    },
});