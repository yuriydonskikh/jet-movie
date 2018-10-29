import { Injectable } from '@angular/core';
import { Http, Response, Headers, URLSearchParams } from '@angular/http';

import { ConfigService } from '../../shared/utils/config.service';

import { BaseService } from '../../shared/services/base.service';

import { CartViewModel, } from '../models/cart-view-model';

import { Observable } from 'rxjs/Rx';

// Add the RxJS Observable operators we need in this app.
import '../../rxjs-operators';

@Injectable()
export class CartService extends BaseService {

    baseUrl: string = '';

    constructor(private http: Http, private configService: ConfigService) {
        super();
        this.baseUrl = configService.getApiURI();
    }

    getAuthHeader(): Headers {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        let authToken = localStorage.getItem('auth_token');
        headers.append('Authorization', `Bearer ${authToken}`);
        return headers;
    }

    getCartItems(): Observable<Array<CartViewModel>> {
        let headers = this.getAuthHeader();

        return this.http.get(this.baseUrl + "/Cart", { headers })
            .map(response => response.json())
            .catch(this.handleError);
    }

    addCartItem(item: CartViewModel): Observable<boolean> {
        let headers = this.getAuthHeader();

        return this.http.post(this.baseUrl + "/Cart", JSON.stringify(item), { headers })
            .map(() => true)
            .catch(this.handleError);
    }

    setPaid(id: number): Observable<boolean> {
        let headers = this.getAuthHeader();

        return this.http.put(this.baseUrl + "/Cart/" + id, null, { headers })
            .map(() => true)
            .catch(this.handleError);
    }

    deleteCartItem(id: number): Observable<boolean> {
        let headers = this.getAuthHeader();

        return this.http.delete(this.baseUrl + "/Cart/" + id, { headers })
            .map(() => true)
            .catch(this.handleError);
    }
}
