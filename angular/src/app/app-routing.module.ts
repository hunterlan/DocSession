import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {AppComponent} from "./app.component";
import {LoginComponent} from "./components/login/login.component";
import {MainComponent} from "./components/main/main.component";
import {DocHomeComponent} from "./components/home/doc-home/doc-home.component";
import {AdminHomeComponent} from "./components/home/admin-home/admin-home.component";
import {HomeComponent} from "./components/home/home.component";

const routes: Routes = [
  {path: '', component: MainComponent},
  {path: 'login', component: LoginComponent},
  {path: 'home', component: HomeComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
