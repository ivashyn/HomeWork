import { Component,OnInit } from '@angular/core';
import {FormsModule} from '@angular/forms';
import { TaskService } from './task.service'
import { Task } from './task'

@Component({
    selector: 'tasks',
    templateUrl: './tasks.component.html',
    styleUrls:['./dasboard.component.css']
    // styleUrls:['./tasks.component.css']
  })
  export class TasksComponent implements OnInit { 
    //hero = 'Hero'; 
    public constructor(private taskservice: TaskService)
    {
    }

    public ngOnInit(){
      this.taskservice.getTasks().then(tasks=>this.tasks=tasks);
    }

   public title= 'Windo';
   public tasks: Task[];
   public selectedTask:Task;
   
    public addTask() {
      //remake  data-bind
      var inputValue = (<HTMLInputElement>document.getElementById("taskText")).value;
      this.taskservice.addTask(inputValue);        
  }

  public onSelected(task:Task) {
      this.taskservice.completeTask(task);
    }

    public delete(task:Task) {
        this.taskservice.removeTask(task.id);
    }
  }