import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { SafeResourceUrl } from '@angular/platform-browser';

@Injectable({
  providedIn: 'root',
})
export abstract class ApiBaseService<T> {
  protected http = inject(HttpClient);
  protected abstract resource: string;
  protected baseUrl = environment.apiBaseUrl;

  getAll(params?: Record<string, string | string[]>): Observable<T[]> {
    return this.http.get<T[]>(`${this.baseUrl}/${this.resource}`, { params });
  }

  getById(id: string): Observable<T> {
    return this.http.get<T>(`${this.baseUrl}/${this.resource}/${id}`);
  }

  create(payload: Omit<T, never>): Observable<T> {
    return this.http.post<T>(`${this.baseUrl}/${this.resource}`, payload);
  }

  update(id: string, payload: T): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${this.resource}/${id}`, payload);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${this.resource}/${id}`);
  }
}
