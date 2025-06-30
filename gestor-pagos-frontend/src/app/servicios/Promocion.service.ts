import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CrearPromocionDTO } from '../modelos/CrearPromocionDTO';

@Injectable({ providedIn: 'root' })
export class PromocionService {
  private apiUrl = 'http://localhost:5243/api/promociones';

  constructor(private http: HttpClient) {}

  crearPromocion(dto: CrearPromocionDTO): Observable<any> {
    return this.http.post(`${this.apiUrl}`, dto);
  }

listarPromociones(ciudad?: string, colonia?: string, paqueteId?: number, tipoServicio?: string): Observable<any[]> {
  let params = new HttpParams();
  if (ciudad) params = params.set('ciudad', ciudad);
  if (colonia) params = params.set('colonia', colonia);
  if (paqueteId !== undefined) params = params.set('paqueteId', paqueteId);
  if (tipoServicio) params = params.set('tipoServicio', tipoServicio);

  return this.http.get<any[]>(this.apiUrl, { params });
}

cambiarEstadoPromocion(id: number, activa: boolean): Observable<any> {
  return this.http.put(`https://localhost:5243/api/promociones/${id}/estado?activa=${activa}`, {});
}

}
