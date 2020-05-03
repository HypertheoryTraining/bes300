import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CurbsideComponent } from './components/curbside/curbside.component';


const routes: Routes = [{
  path: '',
  component: CurbsideComponent
}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
