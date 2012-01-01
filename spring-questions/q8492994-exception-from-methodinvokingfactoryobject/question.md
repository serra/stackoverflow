I have Spring.Configuration like that:

    <objects xmlns="http://www.springframework.net">
        <!-- Create SQLite connection -->
        <object id="SqlConnection" type="System.Data.SQLite.SQLiteConnection, System.Data.SQLite">
            <constructor-arg index="0" value="Data Source=MyDatabase.db"/>
        </object>
        <!-- Open connection -->
        <object type="Spring.Objects.Factory.Config.MethodInvokingFactoryObject, Spring.Core">
            <property name="TargetObject">
                <ref local="SqlConnection" />
            </property>
            <property name="TargetMethod" value="Open"/>
        </object>
    </objects>

So how to handle exception from method Open() if this throws since MethodInvokingFactoryObject is create and invoke?
