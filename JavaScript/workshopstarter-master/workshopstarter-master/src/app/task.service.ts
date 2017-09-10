import{ Injectable } from '@angular/core';
import { Task } from './task'
import { TASKS } from './mock-task'


@Injectable()
export class TaskService{
    public getTasks() :Promise<Task[]>
    {
        return new Promise(resolver=>
        {
            setTimeout(()=>{
                resolver(TASKS);
            },500);
        });
    }
    
    public getTask(id: number) :Promise<Task>
    {
        return new Promise(resolver=>
            {
                const task = TASKS.find(f=>f.id===id);
                setTimeout(()=>{
                    resolver(task);
                },500);
            });
    }

    public addTask(taskText : string) {
        var task = new Task();
        var lastTask = TASKS[TASKS.length-1]; //bad
        var idToAdd = lastTask.id +1;

        task.text = taskText;
        task.isCompleted = false;
        task.id = idToAdd;
        TASKS.push(task);
    }

    public removeTask(id : number) {
        var task = this.getTask(id);
        var index = TASKS.findIndex(t=>t.id == id);
        TASKS.splice(index,1);
        
    }

    public completeTask(task: Task) {
        var task = TASKS.find(f=>f.id===task.id);
        task.isCompleted = !task.isCompleted;
    }
}