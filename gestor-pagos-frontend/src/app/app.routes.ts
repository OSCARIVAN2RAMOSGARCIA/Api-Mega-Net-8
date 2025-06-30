import { Routes } from '@angular/router';
import { DeudaComponent } from './componentes/deuda/deuda.component';
import { ListarPromocionesComponent } from './componentes/promociones/listar-promociones/listar-promociones.component';
import { CrearPromocionComponent } from './componentes/promociones/crear-promocion/crear-promocion.component';
import { CambiarEstadoComponent } from './componentes/promociones/cambiar-estado/cambiar-estado.component';

export const routes: Routes = [
  { path: '', redirectTo: 'promociones', pathMatch: 'full' },
  { path: 'promociones', component: ListarPromocionesComponent },
  // { path: 'crear', component: CrearPromocionComponent },
  { path: 'estado', component: CambiarEstadoComponent },
  { path: 'deuda', component: DeudaComponent }
];


