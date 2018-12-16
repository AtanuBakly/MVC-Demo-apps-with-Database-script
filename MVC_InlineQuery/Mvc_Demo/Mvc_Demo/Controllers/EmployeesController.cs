using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mvc_Demo_EF.Models;
using DataAccessLayer;

namespace Mvc_Demo_EF.Controllers
{
    public class EmployeesController : Controller
    {
       // private MVCTestEntities db = new MVCTestEntities();

        SqlExecute objSqlExecute = new SqlExecute();

        // GET: Employees
        public ActionResult Index()
        {
            //var employees = db.Employees.Include(e => e.Department);
            string sql = "select E.Id,E.Name,E.Gender,E.DepartmentId,D.DeptName from Employee E inner join Department D on E.DepartmentId=D.Id";
            DataSet ds = new DataSet();
            ds = objSqlExecute.DisplayData(sql);
            List<Employee2> employeeList = new List<Employee2>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Employee2 employee = new Employee2();
                employee.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"]);
                employee.Name = Convert.ToString(ds.Tables[0].Rows[i]["Name"]);
                employee.Gender = Convert.ToString(ds.Tables[0].Rows[i]["Gender"]);
                employee.DepartmentName = Convert.ToString(ds.Tables[0].Rows[i]["DeptName"]);
                employee.DepartmentId = Convert.ToInt32(ds.Tables[0].Rows[i]["DepartmentId"]);
                employeeList.Add(employee);
            }
            return View(employeeList.ToList());

           
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Employee employee = db.Employees.Find(id);
            string sql = "select E.Id,E.Name,E.Gender,E.DepartmentId,D.DeptName from Employee E inner join Department D on E.DepartmentId=D.Id where E.Id='" + id + "'";
            DataSet ds = new DataSet();
            ds=objSqlExecute.DisplayData(sql);
            Employee2 employee = new Employee2();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                employee.Id =Convert.ToInt32(ds.Tables[0].Rows[i]["Id"]);
                employee.Name = Convert.ToString(ds.Tables[0].Rows[i]["Name"]);
                employee.Gender = Convert.ToString(ds.Tables[0].Rows[i]["Gender"]);
                employee.DepartmentName = Convert.ToString(ds.Tables[0].Rows[i]["DeptName"]);
                employee.DepartmentId = Convert.ToInt32(ds.Tables[0].Rows[i]["DepartmentId"]);
            }
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            List<Department2> objDepartmentList = ReturnDepartmentList();
            ViewBag.DepartmentId = new SelectList(objDepartmentList, "Id", "DeptName");
            return View();
        }

        private List<Department2> ReturnDepartmentList()
        {
            string sql = "select Id, DeptName from Department";
            DataSet ds = new DataSet();
            ds = objSqlExecute.DisplayData(sql);
            List<Department2> objDepartmentList = new List<Department2>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Department2 objDepartment = new Department2();
                objDepartment.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"]);
                objDepartment.DeptName = Convert.ToString(ds.Tables[0].Rows[i]["DeptName"]);
                objDepartmentList.Add(objDepartment);
            }
            return objDepartmentList;
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee2 employee)//[Bind(Include = "Id,Name,Gender,DepartmentId")] 
        {

            if (ModelState.IsValid)
            {
                string sql = "insert into Employee(Name, Gender, DepartmentId)values('" + employee.Name + "','" + employee.Gender + "'," + employee.DepartmentId + ")";
                int r = objSqlExecute.ExecuteData(sql);
                return RedirectToAction("Index");
            }
            List<Department2> objDepartmentList = ReturnDepartmentList();
            ViewBag.DepartmentId = new SelectList(objDepartmentList, "Id", "DeptName", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Employee employee = db.Employees.Find(id);
            Employee2 employee = ReturnEmployeeList(id);

            if (employee == null)
            {
                return HttpNotFound();
            }
            List<Department2> objDepartmentList = ReturnDepartmentList();
           ViewBag.DepartmentId = new SelectList(objDepartmentList, "Id", "DeptName", employee.DepartmentId);
            return View(employee);
        }

        private Employee2 ReturnEmployeeList(int? id)
        {
            string sql = "select E.Id,E.Name,E.Gender,E.DepartmentId,D.DeptName from Employee E inner join Department D on E.DepartmentId=D.Id where E.Id='" + id + "'";
            DataSet ds = new DataSet();
            ds = objSqlExecute.DisplayData(sql);
            Employee2 employee = new Employee2();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                employee.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"]);
                employee.Name = Convert.ToString(ds.Tables[0].Rows[i]["Name"]);
                employee.Gender = Convert.ToString(ds.Tables[0].Rows[i]["Gender"]);
                employee.DepartmentName = Convert.ToString(ds.Tables[0].Rows[i]["DeptName"]);
                employee.DepartmentId = Convert.ToInt32(ds.Tables[0].Rows[i]["DepartmentId"]);
            }
            return employee;
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee2 employee)//[Bind(Include = "Id,Name,Gender,DepartmentId")] 
        {

            if (ModelState.IsValid)
            {
                //db.Entry(employee).State = EntityState.Modified;
                //db.SaveChanges();
                string sql = "update Employee set Name='" + employee.Name + "',Gender='" + employee.Gender + "',DepartmentId="+employee.DepartmentId+" where Id="+employee.Id+"";
                int r = objSqlExecute.ExecuteData(sql);
                return RedirectToAction("Index");
            }
            List<Department2> objDepartmentList = ReturnDepartmentList();
            ViewBag.DepartmentId = new SelectList(objDepartmentList, "Id", "DeptName", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Employee employee = db.Employees.Find(id);
            Employee2 employee = ReturnEmployeeList(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Employee employee = db.Employees.Find(id);
            //db.Employees.Remove(employee);
            //db.SaveChanges();
            string sql = "DELETE from Employee where Id=" + id + "";
            int k = objSqlExecute.ExecuteData(sql);
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
