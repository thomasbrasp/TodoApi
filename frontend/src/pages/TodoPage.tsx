import z from "zod";
import {zodResolver} from "@hookform/resolvers/zod";
import {type TodoItem, useDeleteApiTodosId, useGetApiTodos, usePostApiTodos, usePutApiTodosIdToggleComplete} from "@/api/endpoints/todo.ts";
import {Controller, type SubmitHandler, useForm} from "react-hook-form";
import {Input} from "@/components/ui/input.tsx";
import {Checkbox} from "@/components/ui/checkbox.tsx";
import {EditDialog} from "@/components/EditDialog.tsx";
import {Button} from "@/components/ui/button.tsx";


const TodoSchema = z
    .object({
        id: z
            .number()
            .optional(),
        name: z
            .string()
            .min(5, {message: 'The todo needs to be at least 5 characters'})
    })

type TodoSchemaType = z.infer<typeof TodoSchema>;


const TodoPage = () => {
    const {data, refetch: refetchTodos} = useGetApiTodos();

    const {mutateAsync: postTodo} = usePostApiTodos({
        mutation: {
            onSuccess: async () => {
                await refetchTodos();
            },
            onError: () => console.log("onError triggered")
        }
    })

    const {mutateAsync: toggleCompleted} = usePutApiTodosIdToggleComplete({
        mutation: {
            onSuccess: async () => {
                await refetchTodos()
            },
            onError: () => console.log("onError toggle completed triggered")
        }
    })

    const {mutateAsync: deleteTodo} = useDeleteApiTodosId({
        mutation: {
            onSuccess: async () => {
                await refetchTodos()
            },
            onError: () => console.log("onError deleteTodo triggered")
        }
    })

    const {control, handleSubmit} = useForm({
        resolver: zodResolver(TodoSchema),
        defaultValues: {
            name: "",
        },
    })

    const onSubmitCreateTodo: SubmitHandler<TodoSchemaType> = (data) => {
        const todo: TodoItem = {
            isComplete: false,
            name: data.name
        }
        console.log(data)
        return postTodo({data: {...todo}})
    }

    return (
        <>
            <h1>TODO APP</h1>
            <form onSubmit={handleSubmit(onSubmitCreateTodo, console.log)}>
                <Controller
                    name="name"
                    control={control}
                    render={({field}) => <Input {...field} className="w-125"/>}
                />
            </form>

            <div className="flex flex-col gap-2">
                {data && data.map((todo) => (
                    <div key={todo.id!} className="flex gap-2">
                        <Checkbox
                            checked={todo.isComplete}
                            onCheckedChange={async () => {
                                await toggleCompleted({id: todo.id!, data: {...todo}})
                                await refetchTodos()
                            }}
                        />

                        <span style={{textDecoration: todo.isComplete ? "line-through" : "none"}}>{todo.name}</span>
                        <EditDialog id={todo.id!} name={todo.name!} />
                        <Button onClick={() => deleteTodo({id: todo.id!})}>Delete</Button>

                    </div>
                    ))}
            </div>

        </>
    )
}

export default TodoPage

