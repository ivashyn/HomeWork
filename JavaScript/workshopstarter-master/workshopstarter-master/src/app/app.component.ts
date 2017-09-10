import { Component,OnInit } from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HeroService} from './hero.service';
import {Hero} from './hero';
import { TaskService } from './task.service'
import { Task } from './task'

@Component({
  selector: 'my-app',
  templateUrl: './app.component.html',
  styleUrls:['./app.component.css']
})
export class AppComponent { 
 public title= 'Home Work';

}

