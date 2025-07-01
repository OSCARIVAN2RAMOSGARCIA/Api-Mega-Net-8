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

// En tu servicio
listarPromociones(
  ciudad?: string, 
  colonia?: string, 
  paqueteId?: number, 
  tipoServicio?: string
): Observable<any[]> {
  let params = new HttpParams();
  
  // Solo agregamos los parámetros que tienen valor
  if (ciudad && ciudad.trim()) params = params.set('ciudad', ciudad.trim());
  if (colonia !== undefined) params = params.set('colonia', colonia.trim());
  if (paqueteId !== undefined) params = params.set('paqueteId', paqueteId.toString());
  if (tipoServicio && tipoServicio.trim()) params = params.set('tipoServicio', tipoServicio.trim());
  console.log(this.http.get<any[]>(this.apiUrl, { params }))
  
  return this.http.get<any[]>(this.apiUrl, { params });
  
}

cambiarEstadoPromocion(id: number, activa: boolean): Observable<any> {
  return this.http.put(`http://localhost:5243/api/promociones/${id}/estado?activa=${activa}`, {});
}

}
