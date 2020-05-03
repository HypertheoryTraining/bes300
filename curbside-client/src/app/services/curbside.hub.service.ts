import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { BehaviorSubject, Observable } from 'rxjs';
@Injectable()
export class CurbsideHubService {

    private hubConnection: signalR.HubConnection;
    private subject$: BehaviorSubject<OrderResponse>;
    constructor() {
        this.hubConnection = new signalR.HubConnectionBuilder().withUrl('http://localhost:1337/curbsidehub').build();

        this.hubConnection.start()
            .then(c => console.log('Hub Connection Started!'))
            .catch(err => console.error(`Error in Hub Connection ${err}`));

        this.subject$ = new BehaviorSubject<OrderResponse>(null);

        this.hubConnection.on('OrderPlaced', (data) => {
            this.subject$.next(data);
            console.log('Got this order on OrderPlaced', data);
        });
        this.hubConnection.on('OrderProcessed', (data) => {

            this.subject$.next(data);
            console.log('Got this order on OrderProcessed', data);
        });
    }

    sendOrder(request: OrderRequest) {
        this.hubConnection.send('PlaceOrder', request);
    }
    getOrder(): Observable<OrderResponse> {
        return this.subject$.asObservable();
    }
}

export interface OrderRequest {
    for: string;
    items: string;
}

export interface OrderResponse {
    id: number;
    for: string;
    items: string;
    status: string;
}
