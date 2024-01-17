using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        DataGridView[] grids;

        Model1 db = new Model1();

        List<Authors> authors;
        List<Books> books;
        List<Orders> orders;
        List<Publishings> publishings;
        List<Workers> workers;

        List<int> id_authors = new List<int>();
        List<int> id_books = new List<int>();
        List<int> id_orders = new List<int>();
        List<int> id_publishings = new List<int>();
        List<int> id_workers = new List<int>();

        public Form1()
        {

            InitializeComponent();
            grids = new DataGridView[]
            {
                dataGridView1,
                dataGridView2,
                dataGridView3,
                dataGridView4,
                dataGridView5
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            authors = db.Authors.ToList();
            authors.ForEach(a => id_authors.Add(a.id));
            books = db.Books.ToList();
            books.ForEach(b => id_books.Add(b.id));
            orders = db.Orders.ToList();
            orders.ForEach(o => id_orders.Add(o.id));
            publishings = db.Publishings.ToList();
            publishings.ForEach(p => id_publishings.Add(p.id));
            workers = db.Workers.ToList();
            workers.ForEach(w => id_workers.Add(w.id));

            Controls_Set();
            DGV_Refresh();
        }

        private void Controls_Set()
        {
            comboBox2_1.Items.Clear();
            comboBox2_1.DisplayMember = nameof(Authors.Surname);
            comboBox2_1.Items.AddRange(authors.ToArray());
            comboBox2_1.SelectedIndex = 0;

            comboBox2_2.Items.Clear();
            comboBox2_2.DisplayMember = nameof(Publishings.Name);
            comboBox2_2.Items.AddRange(publishings.ToArray());
            comboBox2_2.SelectedIndex = 0;

            comboBox2_3.Items.Clear();
            comboBox2_3.Items.Add("Мягкий");
            comboBox2_3.Items.Add("Твердый");
            comboBox2_3.SelectedIndex = 0;

            comboBox3_1.Items.Clear();
            comboBox3_1.DisplayMember = nameof(Workers.Surname);
            comboBox3_1.Items.AddRange(workers.ToArray());
            comboBox3_1.SelectedIndex = 0;

            comboBox3_2.Items.Clear();
            comboBox3_2.DisplayMember = nameof(Books.Name);
            comboBox3_2.Items.AddRange(books.ToArray());
            comboBox3_2.SelectedIndex = 0;

            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].HeaderText = "Фамилия";
            dataGridView1.Columns[1].HeaderText = "Имя";
            dataGridView1.Columns[2].HeaderText = "Отчество (не обязательно)";
            dataGridView1.Columns[3].HeaderText = "Дата рождения";

            dataGridView2.ColumnCount = 5;
            dataGridView2.Columns[0].HeaderText = "Фамилия автора";
            dataGridView2.Columns[1].HeaderText = "Имя автора";
            dataGridView2.Columns[2].HeaderText = "Название книги";
            dataGridView2.Columns[3].HeaderText = "Название издательства";
            dataGridView2.Columns[4].HeaderText = "Вид переплёта";

            dataGridView3.ColumnCount = 5;
            dataGridView3.Columns[0].HeaderText = "Фамилия работника";
            dataGridView3.Columns[1].HeaderText = "Имя работника";
            dataGridView3.Columns[2].HeaderText = "Название книги";
            dataGridView3.Columns[3].HeaderText = "Дата";
            dataGridView3.Columns[4].HeaderText = "Сумма";

            dataGridView4.ColumnCount = 2;
            dataGridView4.Columns[0].HeaderText = "Название";
            dataGridView4.Columns[1].HeaderText = "Адрес";

            dataGridView5.ColumnCount = 6;
            dataGridView5.Columns[0].HeaderText = "Фамилия";
            dataGridView5.Columns[1].HeaderText = "Имя";
            dataGridView5.Columns[2].HeaderText = "Отчество (не обязательно)";
            dataGridView5.Columns[3].HeaderText = "Должность";
            dataGridView5.Columns[4].HeaderText = "Серия паспорта";
            dataGridView5.Columns[5].HeaderText = "Номер паспорта";

            foreach (var grid in grids)
                foreach (DataGridViewColumn col in grid.Columns)
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;

        }

        private void DGV_Refresh()
        {
            foreach (var grid in grids)
                grid.Rows.Clear();

            foreach (var aut in authors)
                dataGridView1.Rows.Add(aut.Surname, aut.Name, aut.Patronymic, aut.Birthday.ToShortDateString());

            foreach (var book in books)
            {
                Authors aut = db.Authors.Where(a => a.id == book.id_Author).FirstOrDefault();
                Publishings pub = db.Publishings.Where(p => p.id == book.id_Publishing).FirstOrDefault();
                dataGridView2.Rows.Add(aut.Surname, aut.Name, book.Name, pub.Name, book.Binding);
            }

            foreach (var ord in orders)
            {
                Workers work = db.Workers.Where(w => w.id == ord.id_Worker).FirstOrDefault();
                Books book = db.Books.Where(b => b.id == ord.id_Book).FirstOrDefault();
                dataGridView3.Rows.Add(work.Surname, work.Name, book.Name, ord.Date.ToShortDateString(), ord.Cost);
            }

            foreach (var pub in publishings)
                dataGridView4.Rows.Add(pub.Name, pub.Adress);

            foreach (var work in workers)
                dataGridView5.Rows.Add(work.Surname, work.Name, work.Patronymic, work.Post, work.Passport_series, work.Passport_number);

            id_authors.Clear();
            authors.ForEach(a => id_authors.Add(a.id));
            id_books.Clear();
            books.ForEach(b => id_books.Add(b.id));
            id_orders.Clear();
            orders.ForEach(o => id_orders.Add(o.id));
            id_publishings.Clear();
            publishings.ForEach(p => id_publishings.Add(p.id));
            id_workers.Clear();
            workers.ForEach(w => id_workers.Add(w.id));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    {
                        Authors author = new Authors();
                        author.Surname = textBox1_1.Text;
                        author.Name = textBox1_2.Text;
                        author.Patronymic = textBox1_3.Text;
                        author.Birthday = Convert.ToDateTime(dateTimePicker1_1.Text);
                        db.Authors.Add(author);
                        db.SaveChanges();
                        authors.Clear();
                        authors = db.Authors.ToList();
                        textBox1_1.Clear();
                        textBox1_2.Clear();
                        textBox1_3.Clear();
                        break;
                    }

                case 1:
                    {
                        Books book = new Books();
                        book.id_Author = ((Authors)(comboBox2_1.SelectedItem)).id;
                        book.Name = textBox2_1.Text;
                        book.id_Publishing = ((Publishings)comboBox2_2.SelectedItem).id;
                        book.Binding = comboBox2_3.SelectedItem.ToString();
                        db.Books.Add(book);
                        db.SaveChanges();
                        books.Clear();
                        books = db.Books.ToList();
                        textBox2_1.Clear();
                        break;
                    }

                case 2:
                    {
                        Orders order = new Orders();
                        order.id_Worker = ((Workers)comboBox3_1.SelectedItem).id;
                        order.id_Book = ((Books)comboBox3_2.SelectedItem).id;
                        order.Date = Convert.ToDateTime(dateTimePicker3_1.Text);
                        order.Cost = Convert.ToDecimal(textBox3_1.Text);
                        db.Orders.Add(order);
                        db.SaveChanges();
                        orders.Clear();
                        orders = db.Orders.ToList();
                        textBox3_1.Clear();
                        break;
                    }

                case 3:
                    {
                        Publishings publishing = new Publishings();
                        publishing.Name = textBox4_1.Text;
                        publishing.Adress = textBox4_2.Text;
                        db.Publishings.Add(publishing);
                        db.SaveChanges();
                        publishings.Clear();
                        publishings = db.Publishings.ToList();
                        textBox4_1.Clear();
                        textBox4_2.Clear();
                        break;
                    }

                case 4:
                    {
                        Workers worker = new Workers();
                        worker.Surname = textBox5_1.Text;
                        worker.Name = textBox5_2.Text;
                        worker.Patronymic = textBox5_3.Text;
                        worker.Post = textBox5_4.Text;
                        worker.Passport_series = textBox5_5.Text;
                        worker.Passport_number = textBox5_6.Text;
                        db.Workers.Add(worker);
                        db.SaveChanges();
                        workers.Clear();
                        workers = db.Workers.ToList();
                        textBox5_1.Clear();
                        textBox5_2.Clear();
                        textBox5_3.Clear();
                        textBox5_4.Clear();
                        textBox5_5.Clear();
                        textBox5_6.Clear();
                        break;
                    }
            }
            Controls_Set();
            DGV_Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    {
                        int id = id_authors[dataGridView1.SelectedRows[0].Index];
                        Authors author = db.Authors.Find(id);
                        author.Surname = textBox1_1.Text;
                        author.Name = textBox1_2.Text;
                        author.Patronymic = textBox1_3.Text;
                        author.Birthday = Convert.ToDateTime(dateTimePicker1_1.Text);
                        db.SaveChanges();
                        authors.Clear();
                        authors = db.Authors.ToList();
                        textBox1_1.Clear();
                        textBox1_2.Clear();
                        textBox1_3.Clear();
                        break;
                    }

                case 1:
                    {
                        int id = id_books[dataGridView2.SelectedRows[0].Index];
                        Books book = db.Books.Find(id);
                        book.id_Author = ((Authors)(comboBox2_1.SelectedItem)).id;
                        book.Name = textBox2_1.Text;
                        book.id_Publishing = ((Publishings)comboBox2_2.SelectedItem).id;
                        book.Binding = comboBox2_3.SelectedItem.ToString();
                        db.SaveChanges();
                        books.Clear();
                        books = db.Books.ToList();
                        textBox2_1.Clear();
                        break;
                    }

                case 2:
                    {
                        int id = id_orders[dataGridView3.SelectedRows[0].Index];
                        Orders order = db.Orders.Find(id);
                        order.id_Worker = ((Workers)comboBox3_1.SelectedItem).id;
                        order.id_Book = ((Books)comboBox3_2.SelectedItem).id;
                        order.Date = Convert.ToDateTime(dateTimePicker3_1.Text);
                        order.Cost = Convert.ToDecimal(textBox3_1.Text);
                        db.SaveChanges();
                        orders.Clear();
                        orders = db.Orders.ToList();
                        textBox3_1.Clear();
                        break;
                    }

                case 3:
                    {
                        int id = id_publishings[dataGridView4.SelectedRows[0].Index];
                        Publishings publishing = db.Publishings.Find(id);
                        publishing.Name = textBox4_1.Text;
                        publishing.Adress = textBox4_2.Text;
                        db.SaveChanges();
                        publishings.Clear();
                        publishings = db.Publishings.ToList();
                        textBox4_1.Clear();
                        textBox4_2.Clear();
                        break;
                    }

                case 4:
                    {
                        int id = id_workers[dataGridView5.SelectedRows[0].Index];
                        Workers worker = db.Workers.Find(id);
                        worker.Surname = textBox5_1.Text;
                        worker.Name = textBox5_2.Text;
                        worker.Patronymic = textBox5_3.Text;
                        worker.Post = textBox5_4.Text;
                        worker.Passport_series = textBox5_5.Text;
                        worker.Passport_number = textBox5_6.Text;
                        db.SaveChanges();
                        workers.Clear();
                        workers = db.Workers.ToList();
                        textBox5_1.Clear();
                        textBox5_2.Clear();
                        textBox5_3.Clear();
                        textBox5_4.Clear();
                        textBox5_5.Clear();
                        textBox5_6.Clear();
                        break;
                    }
            }
            Controls_Set();
            DGV_Refresh();
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (sender == dataGridView1 && dataGridView1.SelectedRows.Count > 0)
            {
                int id = id_authors[dataGridView1.SelectedRows[0].Index];
                Authors author = db.Authors.Find(id);
                textBox1_1.Text = author.Surname;
                textBox1_2.Text = author.Name;
                textBox1_3.Text = author.Patronymic;
                dateTimePicker1_1.Text = author.Birthday.ToShortDateString();
            }

            if (sender == dataGridView2 && dataGridView2.SelectedRows.Count > 0)
            {
                int id = id_books[dataGridView2.SelectedRows[0].Index];
                Books book = db.Books.Find(id);
                textBox2_1.Text = book.Name;
                foreach (Authors a in comboBox2_1.Items)
                {
                    if (a.id == book.id_Author)
                        comboBox2_1.SelectedItem = a;
                }

                foreach (Publishings p in comboBox2_2.Items)
                {
                    if (p.id == book.id_Publishing)
                        comboBox2_2.SelectedItem = p;
                }

                comboBox2_3.SelectedIndex = comboBox2_3.FindStringExact(book.Binding);
            }

            if (sender == dataGridView3 && dataGridView3.SelectedRows.Count > 0)
            {
                int id = id_orders[dataGridView3.SelectedRows[0].Index];
                Orders order = db.Orders.Find(id);
                foreach (Workers w in comboBox3_1.Items)
                {
                    if (w.id == order.id_Worker)
                        comboBox3_1.SelectedItem = w;
                }

                foreach (Books b in comboBox3_2.Items)
                {
                    if (b.id == order.id_Book)
                        comboBox3_2.SelectedItem = b;
                }

                dateTimePicker3_1.Text = order.Date.ToShortDateString();
                textBox3_1.Text = order.Cost.ToString();
            }

            if (sender == dataGridView4 && dataGridView4.SelectedRows.Count > 0)
            {
                int id = id_publishings[dataGridView4.SelectedRows[0].Index];
                Publishings publishing = db.Publishings.Find(id);
                textBox4_1.Text = publishing.Name;
                textBox4_2.Text = publishing.Adress;
            }

            if (sender == dataGridView5 && dataGridView5.SelectedRows.Count > 0)
            {
                int id = id_workers[dataGridView5.SelectedRows[0].Index];
                Workers worker = db.Workers.Find(id);
                textBox5_1.Text = worker.Surname;
                textBox5_2.Text = worker.Name;
                textBox5_3.Text = worker.Patronymic;
                textBox5_4.Text = worker.Post;
                textBox5_5.Text = worker.Passport_series;
                textBox5_6.Text = worker.Passport_number;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {

                case 0:
                    {
                        int id = id_authors[dataGridView1.SelectedRows[0].Index];
                        Authors author = db.Authors.Find(id);
                        db.Authors.Remove(author);
                        db.SaveChanges();
                        authors.Clear();
                        authors = db.Authors.ToList();
                        textBox1_1.Clear();
                        textBox1_2.Clear();
                        textBox1_3.Clear();
                        break;
                    }

                case 1:
                    {
                        int id = id_books[dataGridView2.SelectedRows[0].Index];
                        Books book = db.Books.Find(id);
                        db.Books.Remove(book);
                        db.SaveChanges();
                        books.Clear();
                        books = db.Books.ToList();
                        break;
                    }

                case 2:
                    {
                        int id = id_orders[dataGridView3.SelectedRows[0].Index];
                        Orders order = db.Orders.Find(id);
                        db.Orders.Remove(order);
                        db.SaveChanges();
                        orders.Clear();
                        orders = db.Orders.ToList();
                        textBox3_1.Clear();
                        break;
                    }

                case 3:
                    {
                        int id = id_publishings[dataGridView4.SelectedRows[0].Index];
                        Publishings publishing = db.Publishings.Find(id);
                        db.Publishings.Remove(publishing);
                        db.SaveChanges();
                        publishings.Clear();
                        publishings = db.Publishings.ToList();
                        textBox4_1.Clear();
                        textBox4_2.Clear();
                        break;
                    }

                case 4:
                    {
                        int id = id_workers[dataGridView5.SelectedRows[0].Index];
                        Workers worker = db.Workers.Find(id);
                        db.Workers.Remove(worker);
                        db.SaveChanges();
                        workers.Clear();
                        workers = db.Workers.ToList();
                        textBox5_1.Clear();
                        textBox5_2.Clear();
                        textBox5_3.Clear();
                        textBox5_4.Clear();
                        textBox5_5.Clear();
                        textBox5_6.Clear();
                        break;
                    }
            }

            Controls_Set();
            DGV_Refresh();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            DGV_Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    {
                        DateTime date = new DateTime();
                        DateTime.TryParse(textBox1.Text, out date);
                        List<Authors> finded_aut = db.Authors.Where(a =>
                         a.Surname.ToLower().Contains(textBox1.Text.ToLower())
                        || a.Name.ToLower().Contains(textBox1.Text.ToLower())
                        || a.Patronymic.ToLower().Contains(textBox1.Text.ToLower())
                        || a.Birthday == date).ToList();

                        id_authors.Clear();
                        finded_aut.ForEach(a => id_authors.Add(a.id));

                        dataGridView1.Rows.Clear();
                        foreach (var aut in finded_aut)
                            dataGridView1.Rows.Add(aut.Surname, aut.Name, aut.Patronymic, aut.Birthday.ToShortDateString());


                        dataGridView1.Rows[0].Selected = true;
                        break;
                    }

                case 1:
                    {
                        var new_books = (from b in books
                                         join a in authors on b.id_Author equals a.id
                                         join p in publishings on b.id_Publishing equals p.id
                                         select new { b.id, aut_name = a.Name, aut_sur = a.Surname, name = b.Name, pub_name = p.Name, binding = b.Binding }).ToList();

                        var finded_books = new_books.Where(
                            x => x.aut_name.ToLower().Contains(textBox1.Text.ToLower())
                            || x.aut_sur.ToLower().Contains(textBox1.Text.ToLower())
                            || x.name.ToLower().Contains(textBox1.Text.ToLower())
                            || x.pub_name.ToLower().Contains(textBox1.Text.ToLower())
                            || x.binding.ToLower().Contains(textBox1.Text.ToLower())).ToList();
                        id_books.Clear();
                        finded_books.ForEach(b => id_books.Add(b.id));

                        dataGridView2.Rows.Clear();

                        finded_books.ForEach(p => dataGridView2.Rows.Add(p.aut_sur, p.aut_name, p.name, p.pub_name, p.binding));


                        break;
                    }

                case 2:
                    {
                        var new_orders = (from o in orders
                                          join w in workers on o.id_Worker equals w.id
                                          join b in books on o.id_Book equals b.id
                                          select new { o.id, work_sur = w.Surname, work_name = w.Name, book_name = b.Name, date = o.Date, cost = o.Cost });

                        DateTime date = new DateTime();
                        DateTime.TryParse(textBox1.Text, out date);

                        decimal dec = new decimal();
                        decimal.TryParse(textBox1.Text.Replace(',', '.'), out dec);

                        var finded_orders = new_orders.Where(
                            o => o.work_sur.ToLower().Contains(textBox1.Text.ToLower())
                            || o.work_name.ToLower().Contains(textBox1.Text.ToLower())
                            || o.book_name.ToLower().Contains(textBox1.Text.ToLower())
                            || o.date == date
                            || o.cost == dec).ToList();
                        id_orders.Clear();
                        finded_orders.ForEach(o => id_orders.Add(o.id));

                        dataGridView3.Rows.Clear();
                        finded_orders.ForEach(f => dataGridView3.Rows.Add(f.work_sur, f.work_name, f.book_name, f.date.ToShortDateString(), f.cost.ToString()));



                        break;
                    }

                case 3:
                    {
                        List<Publishings> finded_publishings = db.Publishings.Where(p =>
                        p.Name.ToLower().Contains(textBox1.Text.ToLower())
                        || p.Adress.ToLower().Contains(textBox1.Text.ToLower())).ToList();
                        id_publishings.Clear();
                        finded_publishings.ForEach(p => id_publishings.Add(p.id));

                        dataGridView4.Rows.Clear();
                        foreach (var pub in finded_publishings)
                            dataGridView4.Rows.Add(pub.Name, pub.Adress);



                        break;
                    }

                case 4:
                    {
                        List<Workers> finded_workers = db.Workers.Where(w =>
                        w.Surname.ToLower().Contains(textBox1.Text.ToLower())
                        || w.Name.ToLower().Contains(textBox1.Text.ToLower())
                        || w.Patronymic.ToLower().Contains(textBox1.Text.ToLower())
                        || w.Post.ToLower().Contains(textBox1.Text.ToLower())
                        || w.Passport_series.ToLower().Contains(textBox1.Text.ToLower())
                        || w.Passport_number.ToLower().Contains(textBox1.Text.ToLower())).ToList();
                        id_workers.Clear();
                        finded_workers.ForEach(w => id_workers.Add(w.id));

                        dataGridView5.Rows.Clear();
                        foreach (var wor in finded_workers)
                            dataGridView5.Rows.Add(wor.Surname, wor.Name, wor.Patronymic, wor.Post, wor.Passport_series, wor.Passport_number);



                        break;
                    }
            }
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            List<Authors> new_aut;
            switch (e.ColumnIndex)
            {
                case 0:
                    {
                        new_aut = authors.OrderBy(a => a.Surname).ToList();
                        break;
                    }

                case 1:
                    {
                        new_aut = authors.OrderBy(a => a.Name).ToList();
                        break;
                    }

                case 2:
                    {
                        new_aut = authors.OrderBy(a => a.Patronymic).ToList();
                        break;
                    }

                case 3:
                    {
                        new_aut = authors.OrderBy(a => a.Birthday).ToList();
                        break;
                    }

                default:
                    {
                        new_aut = new List<Authors>();
                        break;
                    }
            }

            id_authors.Clear();
            new_aut.ForEach(a => id_authors.Add(a.id));
            dataGridView1.Rows.Clear();
            new_aut.ForEach(a => dataGridView1.Rows.Add(a.Surname, a.Name, a.Patronymic, a.Birthday.ToShortDateString()));
        }

        private void dataGridView2_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var new_books = (from b in books
                             join a in authors on b.id_Author equals a.id
                             join p in publishings on b.id_Publishing equals p.id
                             select new { b.id, aut_sur = a.Surname, aut_name = a.Name, name = b.Name, pub_name = p.Name, binding = b.Binding }).ToList();

            switch (e.ColumnIndex)
            {
                case 0:
                    {
                        new_books = new_books.OrderBy(b => b.aut_sur).ToList();
                        break;
                    }

                case 1:
                    {
                        new_books = new_books.OrderBy(b => b.aut_name).ToList();
                        break;
                    }

                case 2:
                    {
                        new_books = new_books.OrderBy(b => b.name).ToList();
                        break;
                    }

                case 3:
                    {
                        new_books = new_books.OrderBy(b => b.pub_name).ToList();
                        break;
                    }

                case 4:
                    {
                        new_books = new_books.OrderBy(b => b.binding).ToList();
                        break;
                    }
            }

            id_books.Clear();
            new_books.ForEach(b => id_books.Add(b.id));
            dataGridView2.Rows.Clear();
            new_books.ForEach(p => dataGridView2.Rows.Add(p.aut_sur, p.aut_name, p.name, p.pub_name, p.binding));
        }

        private void dataGridView3_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var new_orders = (from o in orders
                              join w in workers on o.id_Worker equals w.id
                              join b in books on o.id_Book equals b.id
                              select new { o.id, work_sur = w.Surname, work_name = w.Name, book_name = b.Name, date = o.Date, cost = o.Cost }).ToList();

            switch (e.ColumnIndex)
            {
                case 0:
                    {
                        new_orders = new_orders.OrderBy(o => o.work_sur).ToList();
                        break;
                    }

                case 1:
                    {
                        new_orders = new_orders.OrderBy(o => o.work_name).ToList();
                        break;
                    }

                case 2:
                    {
                        new_orders = new_orders.OrderBy(o => o.book_name).ToList();
                        break;
                    }

                case 3:
                    {
                        new_orders = new_orders.OrderBy(o => o.date).ToList();
                        break;
                    }

                case 4:
                    {
                        new_orders = new_orders.OrderBy(o => o.cost).ToList();
                        break;
                    }

            }

            id_orders.Clear();
            new_orders.ForEach(o => id_orders.Add(o.id));
            dataGridView3.Rows.Clear();
            new_orders.ForEach(f => dataGridView3.Rows.Add(f.work_sur, f.work_name, f.book_name, f.date.ToShortDateString(), f.cost.ToString()));
        }

        private void dataGridView4_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            List<Publishings> new_publishings;
            switch (e.ColumnIndex)
            {
                case 0:
                    {
                        new_publishings = publishings.OrderBy(p => p.Name).ToList();
                        break;
                    }

                case 1:
                    {
                        new_publishings = publishings.OrderBy(p => p.Adress).ToList();
                        break;
                    }

                default:
                    {
                        new_publishings = new List<Publishings>();
                        break;
                    }
            }
            id_publishings.Clear();
            new_publishings.ForEach(p => id_publishings.Add(p.id));
            dataGridView4.Rows.Clear();
            new_publishings.ForEach(p => dataGridView4.Rows.Add(p.Name, p.Adress));
        }

        private void dataGridView5_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            List<Workers> new_workers;
            switch (e.ColumnIndex)
            {
                case 0:
                    {
                        new_workers = workers.OrderBy(w => w.Surname).ToList();
                        break;
                    }

                case 1:
                    {
                        new_workers = workers.OrderBy(w => w.Name).ToList();
                        break;
                    }

                case 2:
                    {
                        new_workers = workers.OrderBy(w => w.Patronymic).ToList();
                        break;
                    }

                case 3:
                    {
                        new_workers = workers.OrderBy(w => w.Post).ToList();
                        break;
                    }

                case 4:
                    {
                        new_workers = workers.OrderBy(w => w.Passport_series).ToList();
                        break;
                    }

                case 5:
                    {
                        new_workers = workers.OrderBy(w => w.Passport_number).ToList();
                        break;
                    }

                default:
                    {
                        new_workers = new List<Workers>();
                        break;
                    }
            }

            id_workers.Clear();
            new_workers.ForEach(w => id_workers.Add(w.id));
            dataGridView5.Rows.Clear();
            foreach (var wor in new_workers)
                dataGridView5.Rows.Add(wor.Surname, wor.Name, wor.Patronymic, wor.Post, wor.Passport_series, wor.Passport_number);
        }
    }
}
