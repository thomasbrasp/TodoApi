import { useState } from "react";

import { useGetApiTodos, usePutApiTodosUpdate } from "@/api/endpoints/todo.ts";
import { zodResolver } from "@hookform/resolvers/zod";
import { Controller, type SubmitHandler, useForm } from "react-hook-form";
import z from "zod";

import { Button } from "@/components/ui/button";
import {
  Dialog,
  DialogClose,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { Field, FieldGroup } from "@/components/ui/field";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";

interface EditDialogProps {
  id: number;
  name: string;
}

const TodoSchema = z.object({
  id: z.number().optional(),
  name: z.string().min(5, { message: "The todo needs to be at least 5 characters" }),
});

type TodoSchemaType = z.infer<typeof TodoSchema>;

export function EditDialog({ id, name }: EditDialogProps) {
  const [open, setOpen] = useState(false);
  const { refetch: refetchTodos } = useGetApiTodos();

  const { control, handleSubmit } = useForm({
    resolver: zodResolver(TodoSchema),
    defaultValues: {
      name: name,
    },
  });

  const { mutateAsync: renameTodo } = usePutApiTodosUpdate({
    mutation: {
      onSuccess: async () => {
        setOpen(false);
        await refetchTodos();
      },
      onError: () => console.log("onError renameTodo triggered"),
    },
  });

  const onSubmitRenameTodo: SubmitHandler<TodoSchemaType> = async (data) => {
    const todo = {
      id: id,
      isComplete: false,
      name: data.name,
    };
    await renameTodo({ params: {id: id}, data: todo });
  };

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogTrigger asChild>
        <Button variant="outline">Edit</Button>
      </DialogTrigger>
      <DialogContent className="sm:max-w-sm">
        <form onSubmit={handleSubmit(onSubmitRenameTodo, console.log)}>
          <DialogHeader>
            <DialogTitle>Change todo name</DialogTitle>
            <DialogDescription>Here you can rename your todo&#39;s title</DialogDescription>
          </DialogHeader>
          <FieldGroup>
            <Field>
              <Label htmlFor="todo-name">Todo</Label>
              <Controller name="name" control={control} render={({ field }) => <Input {...field} id="todo-name" />} />
            </Field>
          </FieldGroup>
          <DialogFooter>
            <DialogClose asChild>
              <Button type="button" variant="outline">
                Cancel
              </Button>
            </DialogClose>
            <Button type="submit">Save changes</Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
}
