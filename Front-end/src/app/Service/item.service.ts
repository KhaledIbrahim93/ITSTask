import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ItemResponse } from '../Model/item-response';
import { title } from 'process';

const API_Item_URL = environment.API_URL;
const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
@Injectable()

export class ItemService {

  constructor(private http: HttpClient) { }

  getAllItems(): Observable<any> {

    var response = this.http.get<ItemResponse[]>(API_Item_URL + '/Get');
    return response;
  }
  addItem(title,description): Observable<any> {
    var item=new ItemResponse();
      item.title=title;
      item.description=description
    return this.http.post<any>(API_Item_URL + '/AddItem', item)
  }
  // deleteProduct(id: any): Observable<any> {
  //   return this.http.delete<any>(API_Item_URL + '/Delete/' + id, httpOptions);
  // }
  // getProductById(id: any): Observable<any> {
  //   return this.http.get<any>(API_Item_URL + '/GetById/' + id, httpOptions);
  // }
  // editProduct(prdocut:any,image:any):Observable<any>
  // {
  //   const editdata=new FormData()
  //   editdata.append('file', image);
  //   editdata.append('info', JSON.stringify(prdocut));
  //  return this.http.put<any>(API_PRODUCT_URL + '/Put',editdata)
  // }

}
