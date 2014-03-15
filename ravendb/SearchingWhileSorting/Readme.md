### My problem

I created an index (for `User`s) that uses an analyzed field we can search by, which uses tokenizes the `Name` property of the user.

Then I selected some users from the database like so:

```c#
var query = session.Query<User>.OrderBy(u => u.Name).ToList();
```

To my surprise, the ordering appeared completely random and I had a hard time figuring out what happened.

### tl;dr;

Don't sort on analyzed fields.

Don't sort and search on the same index. RavenDB does not understand how to sort on tokenized fields, such as those used by search. Or as [Itamar puts it at the end of this very interesting thread][1]

> Besides, sorting by an analyzed field basically has the meaning of sorting on relevancy. 

### What's going on?

I broke down my problem into a simple isolated case and did some experiments with various combinations of indexes. I quickly found that:

These situations yield correct results:

 * having no indexes 
 * having an index that orders by the name field (let's call it `UserSortIndex`)
 * having an index that searches by the name (`UserSearchIndex`) and a separate `UserSortIndex` and query
explicitly by the latter (`session.Query<User, UserSortIndex>.OrderBy(...`) 
 * ? having additional non-analyzed fields to sort on in `UserSearchIndex`

This yields incorrect results:

 * having only a `UserSearchIndex`

If RavenDB finds you are sorting on a field, it uses an index if an index exists for that field. If the 




 [1]: https://groups.google.com/forum/#!topic/ravendb/Yhit1CRmPhQ
 [2]: http://ayende.com/blog/152833/orders-search-in-ravendb
 [3]: http://ayende.com/blog/157761/ravendb-awesome-feature-of-the-day-formatted-indexes
 [4]: http://daniellang.net/searching-on-string-properties-in-ravendb/