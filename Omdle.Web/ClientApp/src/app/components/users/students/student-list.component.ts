import { Component, ViewChild } from "@angular/core";
import { MatDialog, MatSort } from "@angular/material";
import { MatTableDataSource } from "@angular/material/table";
import { ActivatedRoute, Router } from "@angular/router";
import { User } from "../../../models/user.model";
import { AuthService } from "../../../services/auth.service";
import { UserListing } from "../../../models/user-listing.model";
import { AdminService } from "../../../services/admin.service";


@Component({
  selector: "app-student-list",
  templateUrl: "./student-list.component.html"
  
})
export class StudentListComponent {
  @ViewChild(MatSort)
  sort: MatSort;
  userListing = new UserListing(0, []);
  isLoggedIn: boolean;
  displayedColumns: string[] = ["firstname", "lastname", "email","button"];
  pageSize = 10;
  dataSource = new MatTableDataSource<User>();

  constructor(
    private authService: AuthService,
    private adminService: AdminService,
    private dialog: MatDialog,
    private route: ActivatedRoute,
    private router: Router
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
    this.adminService.getStudents(pageNumber, postsPerPage)
      .subscribe(result => {
        this.userListing = result;
        this.dataSource.data = result.users;
      });
  }
  createTeacher(id) {
    this.adminService.createTeacher(id).subscribe(result => this.router.navigateByUrl('/students/'));
  }
  
}
