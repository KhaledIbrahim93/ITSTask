import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import {ItemService} from '../../Service/item.service'
import { from } from 'rxjs';
import { ItemResponse } from 'src/app/Model/item-response';
import { DebugRenderer2 } from '@angular/core/src/view/services';
import { Router } from '@angular/router';
import { FormBuilder ,Validators} from '@angular/forms';
import { environment } from 'src/environments/environment';
import * as XLSX from 'xlsx';

const url=environment.HostURL;
@Component({
  selector: 'app-item-details',
  templateUrl: './item-details.component.html',
  styleUrls: ['./item-details.component.css']
})

export class ItemDetailsComponent implements OnInit {
  allItems: ItemResponse[]=[];
  url:string="";
  counter=0;
  @ViewChild('TABLE') TABLE: ElementRef;
  title = 'Excel';
  constructor(private service:ItemService,private router:Router) {
    // this.AllProducts=new ProductResponse();
  }
  ngOnInit() {

   this.GetItems();
   this.url=url
  }

  GetItems()
  {

    this.service.getAllItems().subscribe(data=>{
    this.allItems=data;
    this.divs=data;
   })
  }
  divs: number[] = [];

  createDiv(): void {
    this.counter++
    this.divs.push(this.divs.length);
  }
  AddItem(title,decs)
  {
    debugger
    this.service.addItem(title.value,decs.value).subscribe(res=>{
        this.GetItems()
        })
  }
  removeItem(i)
  {
    debugger;
    this.divs.splice(i,1);
  }
  // deleteProduct(id)
  // {
  //   this.service.deleteProduct(id).subscribe(res=>{
  //    this.allProducts.splice(this.allProducts.indexOf(id));
  //   })
  // }
  // editProduct(id){

  //   this.router.navigate(['/editProduct/'+id]);
  // }
  // search(value)
  // {
  //   this.service.search(value).subscribe(data=>{
  //     if(!data){
  //       this.GetProducts();
  //     }
  //     this.allProducts=data;
  //   })
  // }
  // checkValue(){
  //   alert("dsgdsgdfdfdf");
  // }
}
