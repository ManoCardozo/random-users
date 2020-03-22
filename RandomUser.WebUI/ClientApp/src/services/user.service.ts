import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http'
import { Inject } from '@angular/core';
import { IUserModel } from '../interfaces/IUserModel';
import { retry, catchError } from 'rxjs/operators';
import { throwError, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  public users: IUserModel[];
  private baseUrl: string;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string)
  {
    this.baseUrl = baseUrl
  }

  get(userId: string): Observable<IUserModel> {
    const opts = {
      params: new HttpParams({
        fromObject: { userId: userId }
      })
    };

    return this.http
      .get<IUserModel>(this.baseUrl + 'user/get', opts)
      .pipe(
        retry(1),
        catchError(this.errorHandler));
  };

  getRandom(): Observable<IUserModel> {
    return this.http
      .get<IUserModel>(this.baseUrl + 'user/getRandom')
      .pipe(
        retry(1),
        catchError(this.errorHandler));
  };

  getList(): Observable<IUserModel[]> {
    return this.http
      .get<IUserModel[]>(this.baseUrl + 'user/getList')
      .pipe(
        retry(1),
        catchError(this.errorHandler));
  };

  update(userModel: IUserModel): Observable<void> {
    return this.http
      .put<void>(this.baseUrl + 'user/update', userModel)
      .pipe(
        retry(1),
        catchError(this.errorHandler));
  };

  delete(userId: string): Observable<void> {
    const opts = {
      params: new HttpParams({
        fromObject: { userId: userId }
      })
    };

    return this.http
      .delete<void>(this.baseUrl + 'user/delete', opts)
      .pipe(
        retry(1),
        catchError(this.errorHandler));
  };

  // Error handling
  errorHandler(error) {
    let errorMessage = '';

    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }

    console.log(errorMessage);

    return throwError(errorMessage);
  }
}
