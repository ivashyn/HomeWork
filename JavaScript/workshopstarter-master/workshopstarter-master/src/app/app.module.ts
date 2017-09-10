import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {FormsModule} from '@angular/forms';
import { AppComponent }  from './app.component';
// import {HeroDetailComponent} from './hero-details.component';
// import {HeroService} from './hero.service';
import { TaskService } from './task.service'
//import {HeroesComponent} from './heroes.component';
import { TasksComponent } from './tasks.component'
import {Dasboard} from './dashboard.component';
import {RouterModule,Routes} from '@angular/router'; 

const routes:Routes=[
  {
    path:'',
    component:Dasboard,
    pathMatch:'full'
  },
     {
       path:'dashboard',
       component: Dasboard
     },
     {
       path:'tasks',
       component: TasksComponent
     }//,
    //  {
    //    path:'details/:id',
    //    component: HeroDetailComponent
    //  }
];

@NgModule({
  imports:      [ BrowserModule,FormsModule,RouterModule.forRoot(routes) ],
  declarations: [ 
    AppComponent,
    
    //HeroDetailComponent,
    //  HeroesComponent,
    TasksComponent,
    Dasboard ],
  bootstrap:    [ AppComponent ],
  providers: [TaskService]
})
export class AppModule { }
