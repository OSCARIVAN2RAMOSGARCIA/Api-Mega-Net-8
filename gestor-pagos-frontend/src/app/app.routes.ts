import { Routes } from '@angular/router';
import { ListarPromocionesComponent } from './componentes/deuda/promociones/listar-promociones/listar-promociones.component';
import { CrearPromocionComponent } from './componentes/deuda/promociones/crear-promocion/crear-promocion.component';
import { CambiarEstadoComponent } from './componentes/deuda/promociones/cambiar-estado/cambiar-estado.component';
import { DeudaComponent } from './componentes/deuda/deuda.component';

export const routes: Routes = [
  { path: '', redirectTo: 'promociones', pathMatch: 'full' },
  { path: 'promociones', component: ListarPromocionesComponent },
  // { path: 'crear', component: CrearPromocionComponent },
  { path: 'estado', component: CambiarEstadoComponent },
  { path: 'deuda', component: DeudaComponent }
];


