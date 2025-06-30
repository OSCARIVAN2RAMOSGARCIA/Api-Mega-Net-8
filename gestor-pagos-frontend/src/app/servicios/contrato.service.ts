import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SuscriptorDeudaDTO } from '../modelos/SuscriptorDeudaDTO';

@Injectable({
  providedIn: 'root'
})
export class ContratoService {
  private apiUrl = 'https://localhost:5243/api/contrato';

  constructor(private http: HttpClient) {}

  obtenerDeuda(id: number): Observable<SuscriptorDeudaDTO> {
    return this.http.get<SuscriptorDeudaDTO>(`${this.apiUrl}/${id}/deuda`);
  }
}

