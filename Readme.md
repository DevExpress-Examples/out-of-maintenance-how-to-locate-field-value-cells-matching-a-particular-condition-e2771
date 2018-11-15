<!-- default file list -->
*Files to look at*:

* [Window1.xaml](./CS/Window1.xaml) (VB: [Window1.xaml.vb](./VB/Window1.xaml.vb))
* [Window1.xaml.cs](./CS/Window1.xaml.cs) (VB: [Window1.xaml.vb](./VB/Window1.xaml.vb))
<!-- default file list end -->
# How to locate field value cells matching a particular condition


<p>The following example demonstrates how to handle the <a href="https://documentation.devexpress.com/#WPF/DevExpressXpfPivotGridPivotGridControl_CustomFieldValueCellstopic">CustomFieldValueCells</a> event to locate a specific column/row header identified by its column's/row's summary values.<br /> In this example, a predicate is used to locate a column that contains only zero summary values. The column header is obtained by the event parameter's FindCell method, and then removed via the Remove method.</p>

<br/>


