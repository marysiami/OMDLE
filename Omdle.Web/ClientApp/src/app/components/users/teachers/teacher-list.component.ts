import { Component, ViewChild } from "@angular/core";
import { MatDialog, MatSort } from "@angular/material";
import { MatTableDataSource } from "@angular/material/table";
import { ActivatedRoute } from "@angular/router";
import { User } from "../../../models/user.model";
import { UserListing } from "../../../models/user-listing.model";
import { AuthService } from "../../../services/auth.service";
import { AdminService } from "../../../services/admin.service";


@Component({
  selector: "app-teacher-list",
  templateUrl: "./teacher-list.component.html"
  
})
export class TeacherListComponent {
  @ViewChild(MatSort)
  sort: MatSort;
  userListing = new UserListing(0, []);
  isLoggedIn: boolean;
  displayedColumns: string[] = ["firstname", "lastname", "email"];
  pageSize = 10;
  dataSource = new MatTableDataSource<User>();

  constructor(
    private authService: AuthService,
    private adminService: AdminService,
    private dialog: MatDialog,
    private route: ActivatedRoute
  ) {
  }

  ngOnInit() {
    this.isLoggedIn = this.authService.isLoggedIn();
    this.getUsers();  
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
  }
  
  getUsers(pageNumber = 0, postsPerPage = 10) {
    this.adminService.getTeachers(pageNumber, postsPerPage)
      .subscribe(result => {
        this.userListing = result;
        this.dataSource.data = result.users;
      });
  }

  
}
