import {Component,OnInit} from '@angular/core';
// import {HeroService} from './hero.service';
// import {Hero} from './hero';
import { Task } from './task'
import { TaskService } from './task.service'

@Component({
    selector:'dashboard',
    templateUrl:'./dasboard.component.html',
    styleUrls:['./dasboard.component.css']
})


export class Dasboard implements OnInit
{
    tasks:Task[];
    public constructor(private taskService: TaskService)
    {

    }
    public ngOnInit()
    {
        this.taskService.getTasks().then(tasks=>this.tasks=tasks);
    }
}